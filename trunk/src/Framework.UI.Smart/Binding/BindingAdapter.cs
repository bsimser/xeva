using System.Collections.Generic;
using System.ComponentModel;

namespace XF.UI.Smart
{
   public class BindingAdapter<BindingType> : BindingList<BindingType>, IBindingListView, IRaiseItemChangedEvents
   {
      private IBindingFilter<BindingType> _filter;
      private ListSortDescriptionCollection _sortDescriptions;
      private IList<BindingType> _internalList;
      private List<BindingType> _internalAdditions;
      private List<BindingType> _internalDeletions;

      public BindingAdapter(){}

      public BindingAdapter(IList<BindingType> list)
         : base(list)
      {
         InitializeBindingAdapter(list);
      }

      public string Filter
      {
         get { return _filter.FilterString; }
         set
         {
            _filter.Initialize(value);
            ApplyFilter();
         }
      }

      public ListSortDescriptionCollection SortDescriptions
      {
         get { return _sortDescriptions; }
      }

      public bool SupportsAdvancedSorting
      {
         get { return false; }
      }

      public bool SupportsFiltering
      {
         get { return true; }
      }

      bool IRaiseItemChangedEvents.RaisesItemChangedEvents
      {
         get { return true; }
      }

      public IBindingFilter<BindingType> BindingFilter
      {
         get { return _filter; }
         set { _filter = value; }
      }

      public List<BindingType> Additions
      {
         get { return _internalAdditions; }
         set { _internalAdditions = value; }
      }

      public List<BindingType> Deletions
      {
         get { return _internalDeletions; }
         set { _internalDeletions = value; }
      }

      public void ApplySort(ListSortDescriptionCollection sorts)
      {

      }

      public void RemoveFilter()
      {
         _filter = new BindingFilter<BindingType>();
         ApplyFilter();
      }

      public new bool Remove(BindingType item)
      {
         base.Remove(item);
         if(_internalList.Contains(item))
            _internalDeletions.Add(item);
         if (_internalAdditions.Contains(item))
            _internalAdditions.Remove(item);
         return true;
      }

      public new bool Add(BindingType item)
      {
         base.Add(item);
         if (!_internalList.Contains(item) &&
             !_internalAdditions.Contains(item))
            _internalAdditions.Add(item);
         if (_internalDeletions.Contains(item))
            _internalDeletions.Remove(item);
         return true;
      }

      public new List<BindingType> Items
      {
         get { return new List<BindingType>(_internalList);}
      }

      private void InitializeBindingAdapter(IList<BindingType> list)
      {
         _internalList = new List<BindingType>(list);
         _internalAdditions = new List<BindingType>();
         _internalDeletions = new List<BindingType>();
         _sortDescriptions = new ListSortDescriptionCollection();
         _filter = new BindingFilter<BindingType>();
      }

      private void ApplyFilter()
      {
         Clear();
         foreach (BindingType item in _internalList)
         {
            if (_filter.Properties.Count == 0 ||
                _filter.IncludeItem(item))
               base.Items.Add(item);
         }
         foreach (BindingType addition in _internalAdditions)
         {
            base.Items.Add(addition);
         }
         foreach (BindingType deletion in _internalDeletions)
         {
            base.Items.Remove(deletion);
         }
      }

   }

}