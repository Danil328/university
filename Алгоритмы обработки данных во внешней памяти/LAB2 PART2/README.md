# Lab 2 part 2

* Results:

	```
	Size of int array: 381
	Size of decimal array: 1525
	Size of linked list: 2288

	Factorial of 8 = 40320

	Comparison of different sorts for Array
	=======================================
	  Array size: 1000
	    Selection Sort               0,0030368
	    Insertion Sort               0,0019996
	    BinInsert Sort               0,0020517
	    Bucket Sort                  0,180818

	  Array size: 5000
	    Selection Sort               0,0711929
	    Insertion Sort               0,038181
	    BinInsert Sort               0,0218449
	    Bucket Sort                  0,1162406

	  Array size: 10000
	    Selection Sort               0,2774343
	    Insertion Sort               0,1532904
	    BinInsert Sort               0,085595
	    Bucket Sort                  0,0942518

	  Array size: 20000
	    Selection Sort               1,1317124
	    Insertion Sort               0,6009301
	    BinInsert Sort               0,3344245
	    Bucket Sort                  0,1332695

	  Array size: 30000
	    Selection Sort               2,6667671
	    Insertion Sort               1,4111332
	    BinInsert Sort               0,7551872
	    Bucket Sort                  0,1153609


	Comparison of sorts for Array and Linked List
	=======================================
	  Array size: 1000
	    Insertion Sort (Array)                       0,0018712
	    Insertion Sort (List)                        0,0034661
	    Bucket Sort (Array)                          0,1194308
	    Bucket Sort (List)                           0,0994658

	  Array size: 5000
	    Insertion Sort (Array)                       0,0363237
	    Insertion Sort (List)                        0,0671301
	    Bucket Sort (Array)                          0,0994013
	    Bucket Sort (List)                           0,0956893

	  Array size: 10000
	    Insertion Sort (Array)                       0,1463403
	    Insertion Sort (List)                        0,3292229
	    Bucket Sort (Array)                          0,0966264
	    Bucket Sort (List)                           0,1155893

	  Array size: 20000
	    Insertion Sort (Array)                       0,5995459
	    Insertion Sort (List)                        1,6642565
	    Bucket Sort (Array)                          0,112754
	    Bucket Sort (List)                           0,1249133
	```

* Insertion sort in most cases works faster than Selection sort. Their worst time is "n2", while the insertion sort can also work in linear time, if all the elements are already sorted.
* Insertion sort works faster, since time is saved on already sorted parts, while in selection sort always n2 and full search.
* Binary search speeds up insertion sorting
* Bucket sorting was the fastest.
* Sorting a linked list using inserts should work faster than on simple arrays, since there are no shift operations.
