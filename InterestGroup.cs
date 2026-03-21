using System.Collections.ObjectModel;

namespace pract12_TRPO
{
    public class InterestGroup : ObservableObject
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string? _title;
        public string? Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string? _description;
        public string? Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private ObservableCollection<UserInterestGroup>? _userInterestGroups;
        public ObservableCollection<UserInterestGroup>? UserInterestGroups
        {
            get => _userInterestGroups;
            set => SetProperty(ref _userInterestGroups, value);
        }
    }
}