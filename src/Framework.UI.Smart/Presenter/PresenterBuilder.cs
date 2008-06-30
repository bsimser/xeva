using System;
using System.Drawing;

namespace XF.UI.Smart
{
   public static class New
   {
      public static IWindowManagerBuilder<T> Presenter<T>()
         where T : IPresenter
      {
         return new PresenterBuilder<T>(Locator.Resolve<T>());
      }
      
   }

   public class PresenterBuilder<T> : IWindowBuilder<T>, IWindowManagerBuilder<T>
      where T : IPresenter
   {
      private readonly T _presenter;
      private readonly WindowOptions _windowOptions = new WindowOptions();
      private IWindowManager _manager;
      private IRequest _request;
      private WindowClosedCallback _windowClosedCallback;

      public PresenterBuilder(T presenter)
      {
         _presenter = presenter;
      }

      public IPresenterBuilder<T> ManagedBy<TWindowManager>()
         where TWindowManager : IWindowManager
      {
         _manager = Locator.Resolve<TWindowManager>();
         return this;
      }

      public IPresenterBuilder<T> ManagedBy(IWindowManager windowManager)
      {
         _manager = windowManager;
         return this;
      }

      public IPresenterBuilder<T> WithRequest(IRequest request)
      {
         _request = request;
         return this;
      }

      public IWindowBuilder<T> SetWindowOptions(params Action<WindowOptions>[] options)
      {
         foreach (var option in options)
         {
            option(_windowOptions);
         }
         return this;
      }

      public IWindowBuilder<T> Window
      {
         get { return this; }
      }

      public T Presenter
      {
         get { return _presenter; }
      }

      public T Display()
      {
         _presenter.DisplayIn(_manager, _windowOptions);
         if (_windowClosedCallback != null)
            _presenter.Window.Closed += (s, e) => _windowClosedCallback();
         return _presenter;
      }

      public IWindowBuilder<T> Size(int width, int height)
      {
         _windowOptions.Height = height;
         _windowOptions.Width = width;
         return this;
      }

      public IWindowBuilder<T> Size(Size size)
      {
         return Size(size.Width, size.Height);
      }

      public IWindowBuilder<T> Location(int top, int left)
      {
         return Location(new Point(left, top));
      }

      public IWindowBuilder<T> Location(Point location)
      {
         _windowOptions.Location = location;
         return this;
      }

      public IWindowBuilder<T> ClosedCallback(WindowClosedCallback callback)
      {
         _windowClosedCallback = callback;
         return this;
      }

      public IWindowBuilder<T> Modal
      {
         get
         {
            _windowOptions.Modal = true;
            return this;
         }
      }

      public IWindowBuilder<T> NotModal
      {
         get
         {
            _windowOptions.Modal = false;
            return this;
         }
      }
   }

   public interface IWindowManagerBuilder<T>
      where T : IPresenter
   {
      IPresenterBuilder<T> ManagedBy<TWindowManager>()
         where TWindowManager : IWindowManager;
      IPresenterBuilder<T> ManagedBy(IWindowManager windowManager);
   }

   public interface IPresenterBuilder<T> 
      where T: IPresenter
   {
      IPresenterBuilder<T> WithRequest(IRequest request);
      IWindowBuilder<T> Window { get; }
      IWindowBuilder<T> SetWindowOptions(params Action<WindowOptions>[] options);
      T Presenter { get; }
      T Display();
   }

   public interface IWindowBuilder<T> : IPresenterBuilder<T>
      where T: IPresenter
   {
      IWindowBuilder<T> Modal { get; }
      IWindowBuilder<T> NotModal { get; }
      IWindowBuilder<T> Size(int width, int height);
      IWindowBuilder<T> Size(Size size);
      IWindowBuilder<T> Location(int top, int left);
      IWindowBuilder<T> Location(Point location);
      IWindowBuilder<T> ClosedCallback(WindowClosedCallback callback);
   }

   public delegate void WindowClosedCallback();
}