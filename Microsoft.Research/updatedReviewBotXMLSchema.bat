xsd %1.xml /out:CCTools\ReviewBot
xsd %1.xsd /out:CCTools\ReviewBot /c
sd edit CCTools\ReviewBot\XMLSchema.cs
copy %1.cs CCTools\ReviewBot\XMLSchema.cs