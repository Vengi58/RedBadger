the robotrunbook.txt is a sample file that can be passed as a command line argument to the application.

To keep the models clean while avoiding overcomplicating the code I put the extra small methods to extension methods, and also for fun.

Further improvements:
the input parameters of the application assume correct parameters. It should be prepared for invalid parameters and be more descriptive in the -help.
the application code lacks if possible error checks, invalid input checks or unwanted scenario checks.
the stability and the reliability of the application should be improved with code test on different leveles (unit, component, system)