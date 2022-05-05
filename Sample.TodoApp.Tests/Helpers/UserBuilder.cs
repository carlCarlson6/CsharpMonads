using System.Threading.Tasks;
using Raven.Client.Documents;
using Sample.TodoApp.Auth;

namespace Sample.TodoApp.Tests.Helpers;

public class UserBuilder : DocumentBuilder<User>
{
    private UserId _id = UserId.NewId();
    private UserName _name = null!;
    private Password _password = null!;
    
    public UserBuilder With(UserId id)
    {
        _id = id;
        return this;
    }
    
    public UserBuilder With(UserName name)
    {
        _name = name;
        return this;
    }

    public UserBuilder With(Password password)
    {
        _password = password;
        return this;
    }

    public Task<User> BuildAsync(IDocumentStore documentStore) => 
        BuildAsync(documentStore, new User(_id.ToString(), _name, _password));

    public User Build() => Build(new User(_id.ToString(), _name, _password));
}