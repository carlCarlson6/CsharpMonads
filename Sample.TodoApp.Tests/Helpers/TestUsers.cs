using System.Collections.Generic;
using Sample.TodoApp.Auth;

namespace Sample.TodoApp.Tests.Helpers;

public static class TestUsers
{
    public static readonly User TestUserCarl = 
        new("6f0ca2d4-3622-47df-ae87-9c9db625f10d", UserName.From("carl"), Password.From("P@$$w0RD123987"));

    public static List<User> All = new() { TestUserCarl };
}