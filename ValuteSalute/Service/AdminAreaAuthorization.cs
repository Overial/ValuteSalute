using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace ValuteSalute.Service
{
    public class AdminAreaAuthorization
        : IControllerModelConvention
    {
        private readonly string _area;
        private readonly string _policy;

        public AdminAreaAuthorization(string area, string policy)
        {
            this._area = area;
            this._policy = policy;
        }

        // Set up filter for unauthorized users
        public void Apply(ControllerModel controllerModel)
        {
            if (controllerModel.Attributes.Any(a =>
                    (a is AreaAttribute) && (a as AreaAttribute).RouteValue.Equals(this._area, StringComparison.OrdinalIgnoreCase))
                ||
                controllerModel.RouteValues.Any(r =>
                    r.Key.Equals("area", StringComparison.OrdinalIgnoreCase) && r.Value.Equals(this._area, StringComparison.OrdinalIgnoreCase)))
            {
                controllerModel.Filters.Add(new AuthorizeFilter(this._policy));
            }
        }
    }
}
