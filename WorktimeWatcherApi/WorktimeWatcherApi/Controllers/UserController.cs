﻿using Microsoft.AspNetCore.Mvc;
using WorktimeWatcherApi.Models;
using WorktimeWatcherApi.Services;

namespace WorktimeWatcherApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<List<User>> Get() => await _userService.GetAsync();

    [HttpGet("{user}")]
    public async Task<User> Verify(User user)
    {
        //var user = await _userService.Verify(user);
        throw new NotImplementedException();
    }
    
    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<User>> Get(string id)
    {
        var user = await _userService.GetAsync(id);

        if (user is null)
        {
            return NotFound();
        }
        return user;
    }

    [HttpPost]
    public async Task<IActionResult> Post(User newUser)
    {
        await _userService.CreateAsync(newUser);
        return CreatedAtAction(nameof(Get), new {id = newUser.Id}, newUser);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, User updatedUser)
    {
        var user = await _userService.GetAsync(id);
        if (user is null)
        {
            return NotFound();
        }
        user.Id = user.Id;
        await _userService.UpdateAsync(id, updatedUser);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _userService.GetAsync(id);
        await _userService.RemoveAsync(user.Id);
        return NoContent();
    }
}