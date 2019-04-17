Created By:       Joshua Granger<br>
Date Created:     October 21, 2017

### Instructions for use:
  1. Copy the executable into the directory that contains the CL files to search
  2. Open the command prompt
  3. Type the following:
      C:\path\to\this\exe\_CLsearch.exe TAG1 TAG2 TAG3
      
#### Example input:
  C:\Users\Joshua\Desktop\CLDIRECTORY\_CLsearch.exe 20BLSOL 21BLSOL
  
#### Example output:
>"20BLSOL"<br>
>    FILE: 20BLSOL.CL    LINE: 8<br>
>    FILE: 20BLSOL.CL    LINE: 60<br>
>    FILE: 20BLSOL.CL    LINE: 78<br>
><br>
>"21BLSOL"<br>
>    FILE: 21BLSOL.CL    LINE: 8<br>
>    FILE: 21BLSOL.CL    LINE: 60<br>
>    FILE: 21BLSOL.CL    LINE: 78'''<br>
         
#### Note:
The results of this search are also logged into a file named _CLsearch.txt
     _CLsearch.txt is created in the same directory as _CLsearch.exe<br>
     _CLsearch.txt is OVERWRITTEN every time this application is executed<br>
