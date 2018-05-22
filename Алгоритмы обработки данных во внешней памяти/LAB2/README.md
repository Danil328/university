# Lab 2

* Results:

	```
	Linear steps to find number for array size of 1000000, Average time elapsed in seconds: 0,0163383
		486789 328569 914849 377917 418323 725498 271055 483118 977438 557880
	Binary steps to find number for array size of 1000000, Average time elapsed in seconds: 0,0002741
		18 17 19 19 18 19 13 19 19 19

	Linear steps to find number for linked list size of 1000000, Average time elapsed in seconds: 0,0268101
		43096 212530 480437 242881 133440 107488 339078 414847 202110 752545
	Binary steps to find number for linked list size of 1000000, Average time elapsed in seconds: 0,0732262
		20 17 15 20 18 20 20 17 20 19
	```

	```
	Linear steps to find number for array size of 10000000, Average time elapsed in seconds: 0,1576863
		5312663 6811108 3085443 9737445 5312066 3670212 6586262 1702972 2018441 2053042
	Binary steps to find number for array size of 10000000, Average time elapsed in seconds: 0,0002911
		22 22 20 20 22 23 17 22 21 23

	Linear steps to find number for linked list size of 10000000, Average time elapsed in seconds: 0,3832191
		247124 7587378 2652866 2733344 3374324 7368715 1699986 4686858 6585318 5477822
	Binary steps to find number for linked list size of 10000000, Average time elapsed in seconds: 0,7223122
		23 21 24 18 23 19 22 24 24 21
	```

	```
	Linear steps to find number for array size of 100000000, Average time elapsed in seconds: 1,0693307
		19089519 74499918 54595307 37737457 8680277 17281395 11083421 60726545 48217259 16512396
	Binary steps to find number for array size of 100000000, Average time elapsed in seconds: 0,0003318
		26 25 25 25 26 26 22 17 25 26

	Linear steps to find number for linked list size of 100000000, Average time elapsed in seconds: 3,7004261
		58251299 34871511 82648579 20982319 1642280 33502295 80921825 11648138 32493009 67548426
	Binary steps to find number for linked list size of 100000000, Average time elapsed in seconds: 6,9049087
		25 24 24 26 26 27 26 26 25 26
	```

* 64 bit process
* The binary search is much faster, but in the case of the list, although it finds the number in fewer steps, it works slower, since it is necessary to search for the middle of the segments
* After 200_000_000 array crashes with System.OutOfMemoryException, then use only LinkedList
* Results:

	```
	Linear steps to find number for linked list size of 400000000, Average time elapsed in seconds: 6,1399732
		388209694 108060614 179586483
	Binary steps to find number for linked list size of 400000000, Average time elapsed in seconds: 8,509494
		29 26 27
	```

* LinkedList allows to store more items in memory
