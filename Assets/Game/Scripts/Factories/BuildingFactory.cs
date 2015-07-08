
namespace MS.Factory
{
    public static class Building
    {
        public static MS.Building Create(string type)
        {
            MS.Building building;

            switch (type)
            {
                default:
                    building = null;
                    break;
            }

            return building;
        }
    }
}
