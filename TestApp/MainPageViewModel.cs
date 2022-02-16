using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Vapolia.UserInteraction;
using Xamarin.Essentials;

namespace TestApp
{
	public class MainPageViewModel
    {
        public ObservableCollection<GroupViewModel> Items { get; }

        public MainPageViewModel()
        {
            //Start with 1 group
            Items = new()
            {
                new(10, GenerateItems()),
            };

            //Wait for the UI to be up.
            Task.Delay(1000).ContinueWith(_ =>
            {
				MainThread.BeginInvokeOnMainThread(() =>
				{
		            try
		            {
                        //Remove the 1st group, then add 2 groups
                        //=> triggers a consistency exception in UICollectionView
                        Items.RemoveAt(0);
			            Items.Add(new(100, GenerateItems()));
			            Items.Add(new(200, GenerateItems()));
		            }
		            catch (Exception e)
		            {
			            UserInteraction.Alert($"Crash: {e.Message}");
		            }
				});

            });
        }

        List<ItemViewModel> GenerateItems(int minItems = 0, int maxItems = 5)
        {
	        var r = new Random();
	        var n = r.Next(minItems, maxItems+1);
	        return Enumerable
		        .Repeat(0,n)
		        .Select(_ => r.Next(1000, 10000))
		        .Select(i => new ItemViewModel { ItemId = i })
		        .ToList();
        }
    }


    public class ItemViewModel
    {
        public int ItemId { get; set; }
    }

    public class GroupViewModel : ObservableCollection<ItemViewModel>
    {
        public int GroupId { get; set; }

        public GroupViewModel(int groupId, IEnumerable<ItemViewModel> items) : base(items)
        {
            GroupId = groupId;
        }
    }
}
