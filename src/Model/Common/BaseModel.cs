namespace src.Model.Common;

public abstract class BaseModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
}