


using Dumpify;

// Func<int, string[], (int, string[])> add2 = (int x, string[] log) => (x + 2, [..log, "add2"]);
// Func<int, string[], (int, string[])> mul3 = (int x, string[] log) => (x * 3, [..log, "mul3"]);

// var r1 = add2(1, []);
// var r2 = mul3(r1.Item1, r1.Item2);

// r2.Dump();




var add2 = (int x) => new WithLog(x + 2, ["add2"]);
var mul3 = (int x) => new WithLog(x * 3, ["mul3"]);


var bind = (WithLog w, Func<int, WithLog> fn) => {
    WithLog newValue = fn(w.Value);
    return new WithLog(newValue.Value, [..w.Log, ..newValue.Log]);
};

var r1 = add2(1);
var r2 = bind(r1, mul3);
r2.Dump();

record WithLog(int Value, string[] Log);






// struct WithLog
// {
//     public Func<int, int> fn;

//     public WithLog(Func<int, int> fn)
//     {
//         this.fn = fn;
//     }

//     // public int Run(Read read)
//     // {
//     //     int number = int.Parse(read()!);
//     //     return fn(number);
//     // }

//     public ReadIO Bind(ReadIO next)
//     {
//         var fnCopy = fn;
//         return new(x => next.fn(fnCopy(x)));
//     }

//     public ReadIO Map(Func<int, int> next)
//     {
//         var fnCopy = fn;
//         return new((int x) => next(fnCopy(x)));
//     }
// }

