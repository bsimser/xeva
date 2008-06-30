using System;
using System.Collections.Generic;
using BankTeller.UI.Smart;
using XF.UI.Smart;

namespace BankTeller.UI.Smart
{
   public class MainWindowManager : IWindowManager
   {
      private IWindowAdapter _existingAdapter;
      private readonly WindowFactory _factory;
      private readonly Dictionary<string, IWindowAdapter> _adapters = new Dictionary<string, IWindowAdapter>();

      public MainWindowManager(WindowFactory factory)
      {
         _factory = factory;
      }

      public virtual IWindowAdapter CreateWindowFor(IPresenter presenter)
      {
         if (_existingAdapter != null) _existingAdapter.Hide();
         
         if (_adapters.ContainsKey(presenter.Key))
         {
            var existingAdapter = _adapters[presenter.Key];
            SetExistingAdapter(existingAdapter);
            return existingAdapter;
         }

         var adapter = _factory.Create();

         _adapters.Add(presenter.Key, adapter);
         SetExistingAdapter(adapter);

         return adapter;
      }

      private void SetExistingAdapter(IWindowAdapter existingAdapter)
      {
         _existingAdapter = existingAdapter;
      }
   }
}