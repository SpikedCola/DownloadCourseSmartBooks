Download CourseSmart Books
==========================

A method for downloading books from CourseSmart and converting them to PDF

Overview
--------
CourseSmart is an online book store which lets you buy and rent textbooks. Rented textbooks can be "checked out" so as to be read "offline" through a proprietary viewer. However, there is no way to download a straight PDF file of the book. 

__Please note:__ In order to download a book from CourseSmart, you must either own it, or be renting it from CourseSmart. This is __not__ a way to get free books, only a way to download a book that you already own.

__Also:__ The following was tested in Chrome. Your mileage may vary if you use another browser.

So How Does This Work?
----------------------
When you "check out" a book into your "offline bookshelf", each page of the book is cached in your browser. Then, when you want to view the book, you load their "offline viewer" and it reads the pages out of the cache. We can leverate this feature to download an entire copy of the book for us.

The steps are straightforward:
- First, clear your cache. This is easiest without tons of extraneous files kicking around
- Visit CourseSmart and log in
- "Check out" the book you are interested in
- Finally, use a tool like [ChromeCacheView](http://www.nirsoft.net/utils/chrome_cache_view.html) to extract all the page files from Chrome's cache

*(I assume you will be using ChromeCacheView)*

The files we are interested in have a URL like `www.coursesmart.com/getofflineflashpage/...`. Click the URL tab to sort by URL, highlight all the interesting files, and hit `F4` to bring up the Save dialog. Save these files somewhere. Don't worry about the name, we will deal with this later.

Encrypted Files
---------------
Now, if you're anything like me, you've already opened up one of these `getofflineflashpage` files in a hex editor. You've found some interesting plaintext at the start of the file, but after that comes a bunch of garbage. 

By decompiling `viewer_offline.swf` ([link](http://www.coursesmart.com/viewer_offline.swf)) I was able to find the decryption routine, which was a combination of XOR and byte-swapping. [Link to gist of original code](https://gist.github.com/SpikedCola/e06fca41419df2725007). I have implemented this code in my class `CourseSmartFile`.

Tools
-----
Now for the fun stuff. I have written two tools in C# (.NET 4.0) to process the files we've extracted from the cache. *Please be aware of the fact that the tools are extremely beta, and may spontaneously combust in ways I didn't expect, or care to plan for at the time.*

The first (__"CourseSmart Decrypter"__)[[download](binaries/CourseSmartDecrypter.zip)] will decrypt the `getofflineflashpage` files, and will also rename them based on the page name found in the header. This produces one SWF file per page. The neat thing about this is that, at this stage, we still have the actual text data in the page. __Perhaps someone more talented than I might be able to do better than flattening this data to an image :)__

The second (__"CourseSmart Stitcher"__)[[download](binaries/CourseSmartStitcher.zip)] stitches all the SWF files into one large PDF file. As mentioned above, this simply flattens the SWF into an image, and adds it to a PDF. This is done with a combination of a Shockwave ActiveX object positioned off-screen, and some [PDFSharp](www.pdfsharp.com/PDFsharp/) magic. The resulting PDF file will be ~120-150MB depending on the # of pages in your book. I'm sure this could be optimized, but hey, it works!

Conclusion
----------
So there we have it - "CourseSmart2PDF". It's not the prettiest solution, and it's not fully automated, but it works! 

If you have any comments, suggestions, or improvements, I'd love to hear from you! Send me a pull request, or email me at parkinglotlust[@]gmail.com . Thanks to the nice folks at [/r/ReverseEngineering](http://www.reddit.com/r/reverseengineering) for the encouragement to get this far!
