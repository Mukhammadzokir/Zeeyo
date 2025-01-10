using AutoMapper;
using Zeeyo.Service.Helpers;
using Zeeyo.Domain.Extensions;
using Zeeyo.Data.IRepositories;
using Zeeyo.Service.Exceptions;
using Zeeyo.Domain.Entities.Users;
using Zeeyo.Service.DTOs.Payments;
using Zeeyo.Service.Configurations;
using Microsoft.EntityFrameworkCore;
using Zeeyo.Domain.Entities.Payments;
using Zeeyo.Service.Interfaces.Payments;

namespace Zeeyo.Service.Services.Payments;

public class PaymentService : IPaymentService
{
    private readonly IMapper _mapper;
    private readonly IRepository<User> _studentRepository;
    private readonly IRepository<Payment> _paymentRepository;

    public PaymentService(
        IMapper mapper,
        IRepository<User> studentRepository,
        IRepository<Payment> paymentRepository)
    {
        _mapper = mapper;
        _studentRepository = studentRepository;
        _paymentRepository = paymentRepository;
    }

    public async Task<PaymentForResultDto> AddAsync(PaymentForCreationDto dto)
    {
        var studentData = await _studentRepository
            .SelectAsync(s => s.Id == dto.StudentId);
        if (studentData is null)
            throw new ZeeyoException(404, "Student is not found");

        var paymentData = await _paymentRepository
            .SelectAsync(p => p.StudentId == dto.StudentId);
        if (paymentData is not null)
            throw new ZeeyoException(404, "Payment is already exist");

        var mappedData = _mapper.Map<Payment>(dto);
        mappedData.CreatedAt = TimeHelper.GetCurrentServerTime();

        return _mapper.Map<PaymentForResultDto>(await _paymentRepository.InsertAsync(mappedData));
    }

    public async Task<PaymentForResultDto> ModifyAsync(long id, PaymentForUpdateDto dto)
    {
        var studentData = await _studentRepository
            .SelectAsync(s => s.Id == dto.StudentId);
        if (studentData is null)
            throw new ZeeyoException(404, "Student is not found");

        var paymentData = await _paymentRepository
            .SelectAsync(p => p.Id == id);
        if (paymentData is null)
            throw new ZeeyoException(404, "Payment is not found");

        var mappedData = _mapper.Map(dto, paymentData);
        mappedData.UpdatedAt = TimeHelper.GetCurrentServerTime();
        mappedData.UpdatedBy = HttpContextHelper.UserId;

        await _paymentRepository.UpdateAsync(mappedData);

        return _mapper.Map<PaymentForResultDto>(mappedData);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var paymentData = await _paymentRepository
            .SelectAsync(p => p.Id == id);
        if (paymentData is null)
            throw new ZeeyoException(404, "Payment is not found");

        paymentData.DeletedAt = TimeHelper.GetCurrentServerTime();
        paymentData.DeletedBy = HttpContextHelper.UserId;

        return await _paymentRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<PaymentForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var paymentData = await _paymentRepository
            .SelectAll()
            //.Include(p => p.Student)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<PaymentForResultDto>>(paymentData);
    }

    public async Task<IEnumerable<PaymentForResultDto>> RetrieveAllByStudentIdAsync(long studentId, PaginationParams @params)
    {
        var paymentData = await _paymentRepository
            .SelectAll(p => p.StudentId == studentId)
            //.Include(p => p.Student)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<PaymentForResultDto>>(paymentData);
    }

    public async Task<PaymentForResultDto> RetrieveByIdAsync(long id)
    {
        var paymentData = await _paymentRepository
            .SelectAsync(p => p.Id == id);
        if (paymentData is null)
            throw new ZeeyoException(404, "Payment is not found");

        return _mapper.Map<PaymentForResultDto>(paymentData);
    }

    public async Task<IEnumerable<PaymentForResultDto>> SearchAllAsync(string search, PaginationParams @params)
    {
        var paymentData = await _paymentRepository
            .SelectAll()
            .Where(p => p.Date.ToString().Contains(search.ToString()) || p.StudentId.ToString() == search)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<PaymentForResultDto>>(paymentData);
    }
}
