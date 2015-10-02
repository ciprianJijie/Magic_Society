
using MS.Model;

namespace MS.Controllers.UI
{
    public class PersonalityController : Controller<Views.UI.PersonalityView, Model.Personality>
    {
        public TraitController TraitController;

        public override IUpdatableView<Personality> CreateView(Personality modelElement)
        {
            var view = base.CreateView(modelElement);

            (view as Views.UI.PersonalityView).TraitController = TraitController;

            return view;
        }
    }
}
