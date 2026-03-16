using System;
using System.Linq;

namespace pract12_TRPO.Services
{
    public class UserInterestGroupService
    {
        private readonly AppDbContext _db = BaseDbService.Instance.Context;

        public bool AddStudentToGroup(int studentId, int groupId, DateOnly joinedAt, bool isModerator)
        {
            if (_db.UserInterestGroups.Any(uig =>
                uig.UserId == studentId && uig.InterestGroupId == groupId))
                return false;

            var record = new UserInterestGroup
            {
                UserId = studentId,
                InterestGroupId = groupId,
                JoinedAt = joinedAt,
                IsModerator = isModerator
            };

            _db.UserInterestGroups.Add(record);
            _db.SaveChanges();
            return true;
        }

        public void RemoveStudentFromGroup(int studentId, int groupId)
        {
            var record = _db.UserInterestGroups
                .FirstOrDefault(uig => uig.UserId == studentId && uig.InterestGroupId == groupId);

            if (record != null)
            {
                _db.UserInterestGroups.Remove(record);
                _db.SaveChanges();
            }
        }
    }
}
