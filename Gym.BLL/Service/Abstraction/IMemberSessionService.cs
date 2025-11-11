
using Gym.BLL.ModelVM.Member;
using Gym.BLL.ModelVM.Session;
using Gym.DAL.Entities;

namespace Gym.BLL.Service.Abstraction
{
    public interface IMemberSessionService
    {
        (bool, string) Add(MemberSession memberSession);

        (bool, string) Delete(int id);

        (bool, string) Update(MemberSession memberSession);

        (bool, string, IEnumerable<MemberSession>?) GetAll();

        (bool, string, MemberSession?) GetById(int Id);

        (bool, string, IEnumerable<MemberSession>?) GetByMemberId(int MemberId);
        (bool, string, IEnumerable<MemberSession>?) GetBySessionId(int SessionId);
        (bool, string, IEnumerable<MemberSession>?) GetByTrainerSubscriptionId(int trainerSubscriptionId);
    }
}
