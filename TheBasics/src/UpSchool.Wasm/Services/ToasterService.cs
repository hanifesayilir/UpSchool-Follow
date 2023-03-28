using Blazored.Toast.Services;
using Microsoft.JSInterop;
using Shipwreck.BlazorJqueryToast;
using UpSchool.Domain.Services;

namespace UpSchool.Wasm.Services
{
      public class ToasterService : IToasterService
      {
          private readonly IJSRuntime _jsRuntime;

          public ToasterService(IJSRuntime jSRuntime)
          {
              _jsRuntime = jSRuntime;   
          }

        public void ShowError(string message)
        {
            _jsRuntime.ShowToastAsync(new ToastOptions
            {
                Text = message,
                Position = ToastPosition.TopCenter,
                Heading = "UpSchool"
            });
        }

        public void ShowSuccess(string message)
          {
              _jsRuntime.ShowToastAsync(new ToastOptions
              {
                  Text = message,
                  Position = ToastPosition.TopCenter,
                  Heading = "UpSchool"
              });

          }

      }


   /* public class ToasterService : IToasterService
    {
        private readonly IToastService _toastService;

        public ToasterService(IToastService toastService)
        {
            _toastService = toastService;
        }

        public void ShowSuccess(string message)
        {
            _toastService.ShowSuccess(message);
        }
    }*/
}


