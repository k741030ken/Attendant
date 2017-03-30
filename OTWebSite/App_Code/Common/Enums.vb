'*********************************************************************************
'功能說明：存放所有Enum的常數
'建立人員：Chung
'建立日期：2011.05.12
'*********************************************************************************
Imports Microsoft.VisualBasic

Namespace Bsp
    Public Class Enums
        Enum MessageType
            Errors = 0
            Information = 1
        End Enum

        Enum OrganType
            All
            Dept
            Business
            Branch
        End Enum

        Public Enum FullNameType
            OnlyCode
            OnlyDefine
            CodeDefine
        End Enum

        Public Enum SelectGroupType
            AllGroup                '全部
            HasFun                  '有功能的群組
            NotHasFun               '無功能的群組
        End Enum

        Public Enum SelectFunType
            AllFun                  '全部
            ParentFun               '父功能代碼
            FunHasRight             '傳回有RightID的Fun
            AssignToGroup           '傳回已經Assign給Group的程式
            NotAssignToGroup        '傳回未Assign給Group的程式
        End Enum

        Public Enum SelectCommonType
            All
            Valid
            InValid
        End Enum

        Public Enum SelectStyle
            RadioButton
            CheckBox
        End Enum

        Public Enum GroupFunType
            Group
            Fun
        End Enum

        ''' <summary>
        ''' 功能選取條件類別
        ''' </summary>
        ''' <remarks>
        ''' AllFun:所有功能
        ''' ParentFun:父功能
        ''' AssignToGroup:選取指定群組的功能項目
        ''' FunHasRight:選取有包含Right功能紐的項目
        ''' NotAssignToGroup:選取未授權給指定群組的功能
        ''' </remarks>
        Public Enum SelectFunctionType
            AllFun
            ParentFun
            AssignToGroup
            FunHasRight
            NotAssignToGroup
        End Enum

        Public Enum DateTimeType
            [Date]
            [DateTime]
        End Enum

        Public Enum DisplayType
            OnlyName    '只顯示名字
            OnlyID           '顯示ID  
            Full        '顯示ID + 名字
        End Enum
    End Class
End Namespace
