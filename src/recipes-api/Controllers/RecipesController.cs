using Microsoft.AspNetCore.Mvc;
using recipes_api.Services;
using recipes_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace recipes_api.Controllers;

[ApiController]
[Route("recipe")]
public class RecipesController : ControllerBase
{    
    public readonly IRecipeService _service;
    
    public RecipesController(IRecipeService service)
    {
        this._service = service;        
    }

    // 1 - Sua aplicação deve ter o endpoint GET /recipe
    //Read
    [HttpGet]
    public IActionResult Get()
    {
        List<Recipe> recipes = _service.GetRecipes();
        if (recipes == null || recipes.Count == 0) return NotFound();
        return Ok(recipes);
    }

    // 2 - Sua aplicação deve ter o endpoint GET /recipe/:name
    //Read
    [HttpGet("{name}", Name = "GetRecipe")]
    public IActionResult Get(string name)
    {                
        Recipe recipe = _service.GetRecipe(name);
        if (recipe == null) return NotFound();
        return Ok(recipe);
    }

    // 3 - Sua aplicação deve ter o endpoint POST /recipe
    [HttpPost]
    public IActionResult Create([FromBody]Recipe recipe)
    {
        try
        {
            _service.AddRecipe(recipe);
        }
        catch (System.Exception)
        {
            return NotFound();
        }
        return StatusCode(201, recipe);
    }

    // 4 - Sua aplicação deve ter o endpoint PUT /recipe
    [HttpPut("{name}")]
    public IActionResult Update(string name, [FromBody]Recipe recipe)
    {
        try
        {
            _service.UpdateRecipe(recipe);
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
        return NoContent();
    }

    // 5 - Sua aplicação deve ter o endpoint DEL /recipe
    [HttpDelete("{name}")]
    public IActionResult Delete(string name)
    {
        if (_service.RecipeExists(name))
        {
            _service.DeleteRecipe(name);
            return NoContent();
        }
        return NotFound();
    }    
}
