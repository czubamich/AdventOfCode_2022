public abstract record Step(int from, int to, int amount)
{
    public abstract void Execute(Ship ship);
}
