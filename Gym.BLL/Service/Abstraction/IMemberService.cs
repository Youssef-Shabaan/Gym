
using Gym.BLL.ModelVM.Member;
using Gym.BLL.ModelVM.ModifyPhotos;
using Gym.DAL.Enums;

namespace Gym.BLL.Service.Abstraction
{
    public interface IMemberService
    {
        (bool, string, List<GetMemberVM>) GetAll();
        (bool, string, GetMemberVM) GetByID(int id);
        (bool, string, GetMemberVM) GetByUserID(string id);

        bool ChangePhoto(ChangePhotoVM changePhotoVM);

        void DeletePhoto(int id);

		Task<(bool, string)> Delete(int id);
        (bool, string) Update(EditMemberVM curr);
        Task<(bool, string)> Create(AddMemberVM newmember, bool IsAdmin);

        Task<(bool, string)> CreateMemberForEmail(string name, Gender gender, string image, int age, string address, string userId);
    }
}