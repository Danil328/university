# LAB1

* HDD characteristics: 
	* Name: Western Digital WD Blue (WD10EZEX)
	* Volume: 1 TB
	* Latency: 4 ms
	* RPM: 7200
	* Write/Read speed: 150/150 Mb/s

* RAM characteristics: 
	* Name: G Skill F4-3000C15-8GVGB x2
	* Volume: 16 GB
	* Freq: 3000 Mghz (real 2133, cause i5 SkyLake limitations)
	* Latency: 70-80 ns
	* Write/Read speed 27000/27000 Mb/s
	
* Measures:
	* Speed of the serial read data from disk: 
		* blocksize = 4096 => 70 Mb/s
		* blocksize = 8192 => 115 Mb/s
		* blocksize = 16384 => 157 Mb/s
		* blocksize = 32768 => 159 Mb/s
		* blocksize = 65536 => 160 Mb/s
		* blocksize = 131072 => 158 Mb/s
	* Time to seek of a drive: 7-8 ms
	* Time of random access memory: 140-150 ns
	* Time of the cache/sequential access to the memory: 2200 Mb/s