Questions:
Interview questions:
there are several source csv file in folder "allcores";
Each file have columns such as: ID, FileName, FileExtension, Path, FileSize, CreatedDate, UpdateDate, Other

rows in files are ordered by FileSize descending;


requirement: 
1 ourput all the row in source files into a result.csv file. 
2 result.csv needs to be ordered descding by FileSize;
3 src files might thousands, and result.csv might be GB level;

所使用到的技术点：
1 流读取；
2 事件机制；
3 输出；
4 tostring()和getHash()方法
