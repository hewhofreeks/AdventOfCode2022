I actually liked this problem a lot. Read a couple articles lately about string duplication and how to effeciently use HashSets to do so.

```
HashSet<char> setA = new HashSet(stringA);
HashSet<char> setB = new HashSet(stringB);

```

Will contain our unique characters in each string. So now we can simply loop through one set to find the first duplicate.

Luckily HashSet.Add returns a `bool` value to determine whether or not we're trying to add a duplicate so we can use that!

```
foreach(var c in setA)
{
	if(!setB.Add(c))
		return c;
}
```

This finds our duplicate. We can extend this for Part 2 to utilize this strategy over multiple strings.