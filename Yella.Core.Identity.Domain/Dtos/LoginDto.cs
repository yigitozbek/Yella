﻿ 
namespace Yella.Core.Identity.Dtos;

public class LoginDto 
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool IsRemember { get; set; }
}