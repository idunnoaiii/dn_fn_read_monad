using Dumpify;

var io1 = new ReadIO(x => x * 2)
    .Map(x => x + 5)
    .Map(x => x * 7)
    .Map(x => x * 100);

Console.WriteLine(io1.Run(Console.ReadLine));


delegate string? Read();

struct ReadIO
{
    public Func<int, int> fn;

    public ReadIO(Func<int, int> fn)
    {
        this.fn = fn;
    }

    public int Run(Read read)
    {
        int number = int.Parse(read()!);
        return fn(number);
    }

    public ReadIO Bind(ReadIO next)
    {
        var fnCopy = fn;
        return new(x => next.fn(fnCopy(x)));
    }

    public ReadIO Map(Func<int, int> next)
    {
        var fnCopy = fn;
        return new((int x) => next(fnCopy(x)));
    }
}

struct Function<T>(Func<T, T> fn)
{
    public Func<T, T> run = fn;

    public Function<T> Then(Function<T> next)
    {
        var runCopy = run;
        return new Function<T>(x => next.run(runCopy(x)));
    }

    public static Function<T> operator >>(Function<T> left, Function<T> right)
    {
        return new Function<T>(x => right.run(left.run(x)));
    }
}