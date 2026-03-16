using System;

namespace pract12_TRPO
{
    public class UserInterestGroup : ObservableObject
    {
        private int _userId;
        public int UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }

        private Student? _student;
        public Student? Student
        {
            get => _student;
            set => SetProperty(ref _student, value);
        }

        private int _interestGroupId;
        public int InterestGroupId
        {
            get => _interestGroupId;
            set => SetProperty(ref _interestGroupId, value);
        }

        private InterestGroup? _interestGroup;
        public InterestGroup? InterestGroup
        {
            get => _interestGroup;
            set => SetProperty(ref _interestGroup, value);
        }

        private DateOnly _joinedAt;
        public DateOnly JoinedAt
        {
            get => _joinedAt;
            set => SetProperty(ref _joinedAt, value);
        }

        private bool _isModerator;
        public bool IsModerator
        {
            get => _isModerator;
            set => SetProperty(ref _isModerator, value);
        }
    }
}
