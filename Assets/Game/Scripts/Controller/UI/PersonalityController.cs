using MS.Model;

namespace MS.Controllers.UI
{
    public class PersonalityController : Controller<Views.UI.PersonalityView, Model.Personality>
    {
        public TraitController              TraitController;
        public Heraldry.ShieldController    ShieldController;

        public override IUpdatableView<Personality> CreateView(Personality modelElement)
        {
            Views.UI.PersonalityView personalityView;

            var view = base.CreateView(modelElement);
            
            personalityView                     =   view as Views.UI.PersonalityView;
            personalityView.TraitController     =   TraitController;
            personalityView.ShieldController    =   ShieldController;

            return view;
        }
    }
}
