namespace Contracts.Users.Commands;

public class CreatePerson
{
    public CreatePersonDto Person { get; set; } = new ();
}

public class CreatePersonDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? NickName { get; set; }
    public DateTime? BirthDate { get; set; }
    public bool IsSelectable { get; set; } = false;
    public Guid? UserId { get; set; }
}