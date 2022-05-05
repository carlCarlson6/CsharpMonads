using Raven.Client.Documents.Indexes;
using Sample.TodoApp.Auth;

namespace Sample.TodoApp.Infrastructure.RavenDb.Indexes;

public class Users_ByName : AbstractIndexCreationTask<User, Users_ByName.Result>
{
    public class Result
    {
        public UserName Name { get; set; }
    }
    
    public Users_ByName() => Map = docs => from user in docs select new Result { Name = user.Name };
}
