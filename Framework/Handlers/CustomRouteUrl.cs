using Microsoft.AspNetCore.Mvc.ApplicationModels;

public class CustomRouteUrl : IApplicationModelConvention
{
    public void Apply(ApplicationModel application)
    {
        foreach (var controller in application.Controllers)
        {
            var group = controller.ControllerType.Namespace?.Split('.').Last();
            var handler = controller.ControllerName.Split("Handler").First();

            controller.Selectors[0].AttributeRouteModel = new AttributeRouteModel()
            {
                Template = group + "/" + handler
            };

        }
    }
}