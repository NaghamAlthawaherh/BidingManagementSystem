using BidingManagementSystem.Application.DTOs.Tender;
using BidingManagementSystem.Application.Wrappers;
using BidingManagementSystem.Domain.Entities;
using BidingManagementSystem.Domain.Enums;
using BidingManagementSystem.Persistence;
using Microsoft.Extensions.Hosting;
using BidingManagementSystem.Application.DTOs.Tender;

using Microsoft.EntityFrameworkCore;

namespace BidingManagementSystem.Application.Services
{
    public class TenderService : ITenderService
    {
        private readonly AppDbContext _context;
        private readonly IHostEnvironment _env;


        public TenderService(AppDbContext context, IHostEnvironment _env)
        { 
            _context = context;
           
        }

        public async Task<ServiceResponse<int>> CreateTenderAsync(CreateTenderRequest request)
        {
            var tender = new Tender
            {
                Title = request.Title,
                Description = request.Description,
                Deadline = request.Deadline,
                Budget = request.Budget,
                EligibilityCriteria = request.EligibilityCriteria,
                Industry = request.Industry,
                Location = request.Location,
                Type = request.TenderType,
                CreatedAt = DateTime.UtcNow,
                CreatedByUserId = Guid.NewGuid() // مؤقتًا – لاحقًا استخرج من الـ token
            };

            _context.Tenders.Add(tender);
            await _context.SaveChangesAsync(); // لازم نحفظ أولًا حتى نحصل على TenderId

            // حفظ الملفات
            if (request.Attachments != null && request.Attachments.Any())
            {
                var uploadFolder = Path.Combine(_env.ContentRootPath, "wwwroot", "uploads", "tenders");
                Directory.CreateDirectory(uploadFolder);

                foreach (var file in request.Attachments)
                {
                    var fileName = $"{Guid.NewGuid()}_{file.FileName}";
                    var filePath = Path.Combine(uploadFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var document = new TenderDocument
                    {
                        FileName = file.FileName,
                        FilePath = $"uploads/tenders/{fileName}",
                        FileType = file.ContentType,
                        TenderId = tender.TenderId
                    };

                    _context.TenderDocuments.Add(document);
                }

                await _context.SaveChangesAsync();
            }

            return new ServiceResponse<int>
            {
                Success = true,
                Message = "Tender created successfully",
                Data = tender.TenderId
            };
        }
        public async Task<ServiceResponse<List<TenderDto>>> GetAllTendersAsync()
        {
            var tenders = await _context.Tenders
                .Select(t => new TenderDto
                {
                    TenderId = t.TenderId,
                    Title = t.Title,
                    Description = t.Description,
                    Deadline = t.Deadline,
                    Budget = t.Budget,
                    Industry = t.Industry,
                    Type = t.Type,
                    Location = t.Location,
                    CreatedAt = t.CreatedAt
                })
                .ToListAsync();

            return new ServiceResponse<List<TenderDto>>
            {
                Success = true,
                Message = "Tenders fetched successfully",
                Data = tenders
            };
        }
        
public async Task<ServiceResponse<string>>UpdateTenderAsync(int tenderId, UpdateTenderDto request)
{
    var tender = await _context.Tenders.FindAsync(tenderId);

    if (tender == null)
    {
        return new ServiceResponse<string>
        {
            Success = false,
            Message = "Tender not found"
        };
    }

    // تحديث البيانات
    tender.Title = request.Title;
    tender.Description = request.Description;
    tender.Category = request.Category;
    tender.Industry = request.Industry;
    tender.Location = request.Location;
    tender.EligibilityCriteria = request.EligibilityCriteria;
    tender.Deadline = request.Deadline;
    tender.Budget = request.Budget;
    tender.Type = request.TenderType;

    _context.Tenders.Update(tender);
    await _context.SaveChangesAsync();

    return new ServiceResponse<string>
    {
        Success = true,
        Message = "Tender updated successfully"
    };
}
public async Task<ServiceResponse<string>> DeleteTenderAsync(int tenderId)
{
    var tender = await _context.Tenders.FindAsync(tenderId);

    if (tender == null)
        return new ServiceResponse<string> { Success = false, Message = "Tender not found" };

    // احذف الملفات المرتبطة إذا كان في مرفقات
    var attachments = _context.TenderDocuments.Where(d => d.TenderId == tenderId).ToList();
    foreach (var file in attachments)
    {
        var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "tenders");
        var filePath = Path.Combine(uploadFolder, Path.GetFileName(file.FilePath)); // << تم إصلاح هذا السطر

        if (File.Exists(filePath))
            File.Delete(filePath);
    }

    _context.TenderDocuments.RemoveRange(attachments);
    _context.Tenders.Remove(tender);
    await _context.SaveChangesAsync();

    return new ServiceResponse<string>
    {
        Success = true,
        Message = "Tender deleted successfully"
    };
}
public async Task<ServiceResponse<List<TenderDto>>> GetOpenTendersAsync()
{
    var openTenders = await _context.Tenders
        .Where(t => t.Deadline > DateTime.UtcNow)
        .Select(t => new TenderDto
        {
            TenderId = t.TenderId,
            Title = t.Title,
            Description = t.Description,
            Deadline = t.Deadline,
            Budget = t.Budget,
            Location = t.Location,
            Category = t.Category,
            Industry = t.Industry,
            Type = t.Type
        }).ToListAsync();

    return new ServiceResponse<List<TenderDto>>
    {
        Success = true,
        Data = openTenders
    };
}




    }
}

