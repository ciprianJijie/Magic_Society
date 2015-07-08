
namespace MS.Factory
{
    public static class Building
    {
        public static MS.Building Create(string type)
        {
            MS.Building building;

            switch (type)
            {
                case "city":
                    building = new City();
                    break;

                default:
                    building = null;
                    break;
            }

            return building;
        }
    }
}
