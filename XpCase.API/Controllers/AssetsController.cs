using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using XpCase.Application.DTOs.Asset;
using XpCase.Application.Interfaces;

namespace XpCase.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "v1", IgnoreApi = false)]
[SwaggerTag("Endpoints para gerenciar ativos")]
public class AssetsController : ControllerBase
{
    private readonly IAssetService _assetService;
    private readonly IMapper _mapper;

    public AssetsController(IAssetService assetService, IMapper mapper)
    {
        _assetService = assetService;
        _mapper = mapper;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Obter todos os ativos", Description = "Retorna uma lista de todos os ativos.")]
    [SwaggerResponse(200, "Lista de ativos obtida com sucesso", typeof(IEnumerable<AssetDto>))]
    public async Task<ActionResult<IEnumerable<AssetDto>>> GetAssets()
    {
        var assets = await _assetService.GetAllAsync();
        return Ok(assets);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Obter um ativo", Description = "Retorna os detalhes de um ativo específico pelo ID.")]
    [SwaggerResponse(200, "Ativo obtido com sucesso", typeof(AssetDto))]
    [SwaggerResponse(404, "Ativo não encontrado")]
    public async Task<ActionResult<AssetDto>> GetAsset(Guid id)
    {
        var asset = await _assetService.GetByIdAsync(id);

        if (asset == null) return NotFound(new { message = "Ativo não encontrado" });

        return Ok(asset);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Criar um novo ativo", Description = "Cria um novo ativo com os dados fornecidos.")]
    [SwaggerResponse(201, "Ativo criado com sucesso")]
    [SwaggerResponse(400, "Dados inválidos")]
    [SwaggerResponse(409, "Ativo já existe")]
    public async Task<IActionResult> PostAsset([FromBody] CreateAssetDto createAssetDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var assetExists = await _assetService.AssetExistsAsync(createAssetDto.Symbol);

        if (assetExists) return Conflict(new { message = "Um ativo com este código já existe." });

        var assetId = await _assetService.AddAsync(createAssetDto);

        return CreatedAtAction(nameof(GetAsset), new { id = assetId }, new { message = "Ativo criado com sucesso" });
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Atualizar um ativo", Description = "Atualiza os dados de um ativo existente pelo ID.")]
    [SwaggerResponse(204, "Ativo atualizado com sucesso")]
    [SwaggerResponse(400, "Dados inválidos")]
    public async Task<IActionResult> PutAsset(Guid id, [FromBody] AssetDto assetDto)
    {
        if (id != assetDto.AssetId) return BadRequest("O ID do ativo fornecido não corresponde ao ID na URL.");

        if (!ModelState.IsValid) return BadRequest(ModelState);

        var assetExists = await _assetService.AssetExistsAsync(assetDto.Symbol, id);

        if (assetExists) return Conflict(new { message = "Um ativo com este código já existe." });

        await _assetService.UpdateAsync(assetDto);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Excluir um ativo", Description = "Exclui um ativo existente pelo ID.")]
    [SwaggerResponse(204, "Ativo excluído com sucesso")]
    [SwaggerResponse(400, "Erro ao excluir o ativo")]
    public async Task<IActionResult> DeleteAsset(Guid id)
    {
        try
        {
            await _assetService.DeleteAsync(id);

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("assets-with-positive-value-and-valid-date")]
    [SwaggerOperation(Summary = "Obter todos os ativos com quantidade maior que zero dentro da data de vencimento",
        Description = "Retorna uma lista de todos os ativos com quantidade maior que zero.")]
    [SwaggerResponse(200, "Lista de ativos obtida com sucesso", typeof(IEnumerable<AssetDto>))]
    public async Task<ActionResult<IEnumerable<AssetDto>>> GetActiveAssetsWithPositiveValueAndValidDateAsync()
    {
        var assets = await _assetService.GetAssetsWithPositiveValueAndValidDateAsync();
        return Ok(assets);
    }
}