---------------------------------------------------------------------------------------------------------------------------------------
2008-06-12 D:\Cpms\APV\X060422\Schedule

 JHOME 左維揚 生產日統計表說明

  1. Efficiency    乾部生產的時間  &  良品 長度, 面積, 重量 合計

      良品長度 = 每筆訂單 良品張數 X 每張切長  加總

      良品面積 = 每筆訂單 良品長度 X 原紙寬度  加總

      良品重量 = 每筆訂單 良品面積 X 原紙克重  加總



  2. Wet-End     各車用紙 由 SPEED SENSOR 測量各車用紙長度 X 原紙寬度 X 原紙克重  加總



  3. Dry-End      乾部生產的 良品 & 不良品 長度, 面積, 重量 合計


   chi 2008-6-12

 ---------------------------------------------------------------------------------------------------------------------------------------
2008-06-07 D:\Cpms\APV\X060422\Schedule

 JHOME 左維揚

  1) update Schedule_E

  2) check 在 Product Report 最後有重量 (weight) 欄位

  3) system parameter column(19)

     REPORT_TYPE = 0, 1, 2, 3, 4, 5, 6

     請測試 REPORT_TYPE = ?  比較適合客戶的需求, 再來更改 ?

 chi 2008-6-07

  19   2.00   REPORT_TYPE      = the format for day report list (0, 1, 2, 3)
                                     = 4 for ISOPACK //2006-1-3
                                     = 5 for S&D //2006-4-22
                                     = 6 for InterState Tailand 2008-4-4
---------------------------------------------------------------------------------------------------------------------------------------
2007-07-05 D:\Cpms\APV\X060422\Schedule KKM read schedule.DBF from DataBaseName=PCSDBF TableName=SCHEDULE
                                         copy the source file (4th LINE of the IMPORTPATH.TXT) to the destination file (5th LINE of the IMPORTPATH.TXT) before Import
---------------------------------------------------------------------------------------------------------------------------
2007-05-25 D:\Cpms\APV\X060422\Schedule  Federal auto calculate the number of  out
--------------------------------------------------------------------------------------------------------------
2006-11-23 D:\Cpms\APV\X060422\Schedule\Schedule_E2 STL for check daily report but can not edit the schedule
