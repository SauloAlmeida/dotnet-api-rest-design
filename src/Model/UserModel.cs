using src.Model.Common;

namespace src.Model;

public class UserModel : BaseModel
{
    public string Name { get; set; }

    public IList<UserInfoModel> Infos { get; set; } = new List<UserInfoModel>();
}