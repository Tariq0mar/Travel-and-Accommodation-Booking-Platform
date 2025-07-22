using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Services;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.WebAPI.Models.Payment;

namespace TABP.WebAPI.Controllers;

[ApiController]
[Route("api/payment")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;
    private readonly IMapper _mapper;

    public PaymentController(IPaymentService paymentService, IMapper mapper)
    {
        _paymentService = paymentService ?? throw new ArgumentException(nameof(paymentService));
        _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PaymentResponseDto>> GetById(int id)
    {
        var payment = await _paymentService.GetByIdAsync(id);

        var dto = _mapper.Map<PaymentResponseDto>(payment);
        return Ok(dto);
    }

    [HttpGet("payment-search")]
    public async Task<ActionResult<IEnumerable<PaymentResponseDto>>> GetFiltered([FromQuery] PaymentFilterDto query)
    {
        var newQuery = _mapper.Map<PaymentFilter>(query);

        var filteredPayments = await _paymentService.GetAllAsync(newQuery);
        var dtos = filteredPayments
            .Select(x => _mapper.Map<PaymentResponseDto>(x));

        return Ok(dtos);
    }

    [HttpPost]
    public async Task<ActionResult<PaymentResponseDto>> Create([FromBody] PaymentRequestDto payment)
    {
        var newPayment = _mapper.Map<Payment>(payment);

        var addedPayment = await _paymentService.AddAsync(newPayment);

        var dto = _mapper.Map<PaymentResponseDto>(addedPayment);

        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] PaymentRequestDto payment)
    {
        var newPayment = _mapper.Map<Payment>(payment);
        newPayment.Id = id;

        await _paymentService.UpdateAsync(newPayment);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _paymentService.DeleteAsync(id);
        return NoContent();
    }
}
