namespace TinyFpTest.Examples.Basics.Catalog
{
    public interface ICache
    {
        Catalog Get();
        void Set(Catalog catalog);
    }
}
