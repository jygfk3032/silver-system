Imports System.IO

Class MainWindow
    Dim 抽奖次数 As Integer = 1
    Dim 测试 As Integer = 1
    Dim 姓名列表 As String()
    Dim rnd As New Random(CInt(DateTime.Now.Ticks And &HFFFF))
    Private threeStarItems() As String = {“弹弓”, “鸦羽弓”, “讨龙英杰谭”, “黑缨枪”, “沐浴龙血的剑”, “飞天御剑”, “冷刃”, “神射手之誓”, “翡玉法球”, “魔导绪论”, “以理服人”, “铁影阔剑”, “黎明神剑”} ' 三星物品数组
    Private fourStarItems() As String = {“菲米尼”, “琳妮特”, “卡维”, “米卡”, “瑶瑶”, “珐露珊”, “莱依拉”, “坎蒂丝”, “多莉”, “柯莱”, “久岐忍”, “云堇”, “绮良良”, “鹿野院平藏”, “九条裟罗”, “五郎”, “早柚”, “托马”, “烟绯”, “罗莎莉亚”, “辛焱”, “砂糖”, “迪奥娜”, “重云”, “诺艾尔”, “班尼特”, “菲谢尔”, “凝光”, “行秋”, “北斗”, “香菱”, “安柏”, “雷泽”, “凯亚”, “芭芭拉”, “丽莎”, “弓藏”, “祭礼弓”, “绝弦”, “西风猎弓”, “昭心”, “祭礼残章”, “流浪乐章”, “西风秘典”, “西风长枪”, “匣里灭辰”, “雨裁”, “祭礼大剑”, “钟剑”, “西风大剑”, “匣里龙吟”, “祭礼剑”, “笛剑”, “西风剑”} ' 四星物品数组
    Private fiveStarItems() As String = {“迪希雅”, “提纳里”, “刻晴”, “莫娜”, “七七”, “迪卢克”, “琴”, “阿莫斯之弓”, “天空之翼”, “四风原典”, “天空之卷”, “和璞鸢”, “天空之脊”, “狼的末路”, “天空之傲”, “天空之刃”, “风鹰剑”} ' 五星物品数组
    Private counter As Integer = 0 ' 抽奖计数器
    Private fourStarCounter As Integer = 0 ' 四星保底计数器

    Public Function Wish() As String
        Dim results As New System.Text.StringBuilder()
        Dim rand As New Random()
        Dim 五星 As String
        For i As Integer = 1 To 抽奖次数
            counter += 1
            fourStarCounter += 1

            Dim chance As Double = rand.NextDouble()

            ' 五星物品概率计算
            Dim fiveStarChance As Double = If(counter < 74, 0.006, 1 - Math.Pow(0.94, counter - 73))
            If counter = 90 Then fiveStarChance = 1

            ' 四星物品概率计算
            Dim fourStarChance As Double = 0.051

            ' 判断是否抽到五星物品
            If chance < fiveStarChance Then
                五星 = fiveStarItems(rand.Next(fiveStarItems.Length))
                results.AppendLine(五星)
                标签7.Content = 五星 + “（” + counter.ToString() + “）” + Chr（13） + 标签7.Content
                ' 重置五星计数器和四星保底计数器
                counter = 0
                fourStarCounter = 0
                Continue For
            End If

            ' 判断是否抽到四星物品
            If fourStarCounter >= 10 OrElse chance < fourStarChance + fiveStarChance Then
                fourStarCounter = 0 ' 重置四星保底计数器
                results.AppendLine(fourStarItems(rand.Next(fourStarItems.Length)))
                Continue For
            End If

            ' 默认返回三星物品
            results.AppendLine(threeStarItems(rand.Next(threeStarItems.Length)))
        Next
        标签9.Content = “4星已垫” + Str(fourStarCounter) + "抽"
        标签8.Content = “5星已垫” + Str(counter) + "抽"
        Return results.ToString()
    End Function

    ' 增加抽奖次数
    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        If 姓名列表 Is Nothing Then
            抽奖次数 = 抽奖次数 + 1
            标签1.Content = 抽奖次数.ToString()
            If 抽奖次数 = 5 Then
                按钮3.IsEnabled = False
            End If
        End If
        ' 检查抽奖次数是否已经等于姓名列表的长度
        If 姓名列表 IsNot Nothing AndAlso 抽奖次数 < 姓名列表.Length Then
            抽奖次数 += 1
            标签1.Content = 抽奖次数.ToString()
            ' 当抽奖次数达到姓名列表的长度时禁用按钮3
            If 抽奖次数 = 姓名列表.Length Then
                按钮3.IsEnabled = False
            End If
            If 抽奖次数 = 12 Then
                按钮3.IsEnabled = False
            End If

        End If
        按钮1.IsEnabled = True
    End Sub

    ' 减少抽奖次数
    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        抽奖次数 -= 1
        标签1.Content = 抽奖次数.ToString()
        If 抽奖次数 = 1 Then
            ' 当抽奖次数减至1时禁用按钮1
            按钮1.IsEnabled = False
        End If
        按钮3.IsEnabled = True
    End Sub

    ' 开始抽奖
    ' 开始抽奖
    Private Sub 按钮2_Click(sender As Object, e As RoutedEventArgs) Handles 按钮2.Click
        If 单选框1.IsChecked = False And 单选框2.IsChecked = False And 单选框3.IsChecked = False And 单选框4.IsChecked = False Then
            MsgBox("请选择一个抽奖池", , "提示")
            Return
        End If
        Dim 抽奖结果 As String = ""
        Dim 已抽过的名字 As New Dictionary(Of String, Integer)
        ' 读取之前保存的已抽取次数
        If File.Exists("save.txt") Then
            Using reader As New StreamReader("save.txt")
                Dim line As String
                Do While reader.Peek() >= 0
                    line = reader.ReadLine()
                    Dim parts As String() = line.Split(","c)
                    已抽过的名字(parts(0)) = Integer.Parse(parts(1))
                Loop
            End Using
        End If

        Dim 名字临时列表 As New List(Of String)(姓名列表)
        Dim 未抽中名单 As New List(Of String)(姓名列表)
        For i As Integer = 1 To 抽奖次数
            Dim index As Integer
            Dim 最大概率 As Integer = 10 ' 最大概率
            Dim 概率降低因子 As Integer = 3 ' 每次抽到的概率降低因子
            Dim 概率提高因子 As Integer = 2 ' 每次未抽到的概率提高因子
            Dim 抽中的名字 As String

            ' 调整未抽中名单的概率
            Dim 调整后的概率 As New List(Of Integer)
            For Each 名字 In 未抽中名单
                Dim 新概率 As Integer = 最大概率
                If 已抽过的名字.ContainsKey(名字) Then
                    新概率 -= 已抽过的名字(名字) * 概率降低因子
                Else
                    新概率 += 概率提高因子
                End If
                ' 确保新概率不小于1
                新概率 = Math.Max(新概率, 1)
                调整后的概率.Add(新概率)
            Next
            index = GetWeightedRandomIndex(调整后的概率)
            抽中的名字 = 未抽中名单(index)

            ' 更新已抽取次数
            If 已抽过的名字.ContainsKey(抽中的名字) Then
                已抽过的名字(抽中的名字) += 1
            Else
                已抽过的名字.Add(抽中的名字, 1)
            End If

            ' 将抽中的名字添加到结果字符串中
            抽奖结果 &= 抽中的名字 & If(i Mod 3 = 0, vbCrLf, If(i < 抽奖次数, ", ", ""))
            名字临时列表.Remove(抽中的名字) ' 移除已抽中的名字
            未抽中名单.Remove(抽中的名字) ' 从未抽中名单中移除
        Next

        ' 将已抽取次数保存到文件中
        Using writer As New StreamWriter("save.txt")
            For Each kvp As KeyValuePair(Of String, Integer) In 已抽过的名字
                writer.WriteLine($"{kvp.Key},{kvp.Value}")
            Next
        End Using

        ' 将抽奖结果显示在标签5中
        标签5.Content = 抽奖结果
    End Sub

    ' 根据权重列表随机获取索引
    Private Function GetWeightedRandomIndex(weights As List(Of Integer)) As Integer
        Dim totalWeight As Integer = weights.Sum()
        Dim randomNumber As Integer = rnd.Next(1, totalWeight + 1)
        Dim cumulativeWeight As Integer = 0

        For i As Integer = 0 To weights.Count - 1
            cumulativeWeight += weights(i)
            If randomNumber <= cumulativeWeight Then
                Return i
            End If
        Next

        ' 默认返回最后一个索引
        Return weights.Count - 1
    End Function
 '以下人名均由chat GPT生成

   Private Sub 单选框2_Checked(sender As Object, e As RoutedEventArgs) Handles 单选框2.Checked
    姓名列表 = {"张宇峰", "李鑫海", "王志杰", "赵冠宇", "刘翔浩", "吴俊明", "徐嘉良", "杨泽豪", "韩天宇", "邱昊宇", "沈家铭", "周文昊", "叶承博", "林浩阳", "梁天宇", "孙文杰", "谭嘉成", "冯尚涵", "高宇翔", "吕瀚宇", "杜景伟", "彭泽龙", "苏昊东", "唐伟豪", "方俊杰", "秦廷睿", "夏卓文", "顾彦锦", "许明杰", "任子铭", "曹怡曼", "邹瑞豪", "郭浩天", "黄志源", "李卓宇", "宁晓君", "乔煜", "杨涵韵", "邵福然", "熊亦宸", "冉彩奕", "孔湘怡", "张伊蕾", "郑语嫣", "周若颖", "周清蓉", "赵昭仪"}
    If 抽奖次数 = 11 And 按钮3.IsEnabled = False Then
        按钮3.IsEnabled = True
    End If
    祈愿.Visibility = Visibility.Hidden
End Sub

Private Sub 单选框1_Checked(sender As Object, e As RoutedEventArgs) Handles 单选框1.Checked
    姓名列表 = {"陶翌洲", "田子隽", "沈智铭", "吴俊超", "方若虚", "徐灿", "贺镇鸿", "辛奕锋", "马田田", "洪家维", "何凯", "黎宏鎛", "张思涵", "范威", "蒋晓星", "傅皓喆", "孟天一", "宋子墨", "胡超海", "朱宽", "谭奕轩", "吴宇轩", "金锦程", "皮奕信", "邹翊宇", "赖子万", "萧政岐", "齐松均", "徐文博", "严一涵", "杨昊霖", "余牧阳", "刘九恒", "赵浩宇", "田家辉", "钟紫嫣", "魏奕萌", "陈璨", "李芷墨", "林玉芳", "罗楚妍", "唐思佳", "韩小汐", "谢宇轩", "高紫萌", "宋紫萱", "阳澜", "张静怡", "田子妍"}
    If 抽奖次数 = 11 And 按钮3.IsEnabled = False Then
        按钮3.IsEnabled = True
    End If
    祈愿.Visibility = Visibility.Hidden
End Sub

Private Sub 单选框3_Checked(sender As Object, e As RoutedEventArgs) Handles 单选框3.Checked
    姓名列表 = {"赵宇航", "刘超阳", "陈若虚", "李宏鎛", "吴浩聪", "曹子墨", "顾子隽", "吴智铭", "阳林", "王子万", "袁一涵", "李晓星", "高紫萱", "邹灿", "谢松均", "唐翌洲", "钟欢", "杨小汐", "何紫嫣"}
    If 抽奖次数 = 11 And 按钮3.IsEnabled = False Then
        按钮3.IsEnabled = True
    End If
    祈愿.Visibility = Visibility.Hidden
End Sub

Private Sub 单选框4_Checked(sender As Object, e As RoutedEventArgs) Handles 单选框4.Checked
    姓名列表 = {"胡泽寰", "宋涵韵", "朱尚涵", "唐廷睿", "郑子铭", "邓熙然", "林子俊", "段王骏", "马志源", "韩景伟", "方嘉成", "李冠梁", "张奕", "曹心豪", "高俊逸", "钱灏钰", "顾瀚宇"}
    祈愿.Visibility = Visibility.Hidden
End Sub


    Private Sub 祈愿_Click(sender As Object, e As RoutedEventArgs) Handles 祈愿.Click
        标签5.Content = Wish（）
    End Sub

    Private Sub 单选框5_Checked(sender As Object, e As RoutedEventArgs) Handles 单选框5.Checked
        祈愿.Visibility = Visibility.Visible
        姓名列表 = Nothing
        threeStarItems = {“弹弓”, “鸦羽弓”, “讨龙英杰谭”, “黑缨枪”, “沐浴龙血的剑”, “飞天御剑”, “冷刃”, “神射手之誓”, “翡玉法球”, “魔导绪论”, “以理服人”, “铁影阔剑”, “黎明神剑”} ' 三星物品数组
        fourStarItems = {“菲米尼”, “琳妮特”, “卡维”, “米卡”, “瑶瑶”, “珐露珊”, “莱依拉”, “坎蒂丝”, “多莉”, “柯莱”, “久岐忍”, “云堇”, “绮良良”, “鹿野院平藏”, “九条裟罗”, “五郎”, “早柚”, “托马”, “烟绯”, “罗莎莉亚”, “辛焱”, “砂糖”, “迪奥娜”, “重云”, “诺艾尔”, “班尼特”, “菲谢尔”, “凝光”, “行秋”, “北斗”, “香菱”, “安柏”, “雷泽”, “凯亚”, “芭芭拉”, “丽莎”, “弓藏”, “祭礼弓”, “绝弦”, “西风猎弓”, “昭心”, “祭礼残章”, “流浪乐章”, “西风秘典”, “西风长枪”, “匣里灭辰”, “雨裁”, “祭礼大剑”, “钟剑”, “西风大剑”, “匣里龙吟”, “祭礼剑”, “笛剑”, “西风剑”} ' 四星物品数组
        fiveStarItems = {“迪希雅”, “提纳里”, “刻晴”, “莫娜”, “七七”, “迪卢克”, “琴”, “阿莫斯之弓”, “天空之翼”, “四风原典”, “天空之卷”, “和璞鸢”, “天空之脊”, “狼的末路”, “天空之傲”, “天空之刃”, “风鹰剑”} ' 五星物品数组
    End Sub

    Private Sub 单选框5复制__C__Checked(sender As Object, e As RoutedEventArgs) Handles 单选框5复制__C_.Checked
        threeStarItems = {"物穰 (3)", "嘉果 (3)", "蕃息 (3)", "轮契 (3)", "调和 (3)", "齐颂 (3)", "戍御 (3)", "开疆 (3)", "琥珀 (3)", "相抗 (3)", "锋镝 (3)", "离弦 (3)", "智库 (3)", "灵钥 (3)", "睿见 (3)", "乐圮 (3)", "天倾 (3)", "俱殁 (3)", "渊环 (3)", "幽邃 (3)", "匿影 (3)"}
        fourStarItems = {"此时恰好 (4)", "同一种心情 (4)", "一场术后对话 (4)", "舞！舞！舞！ (4)", "记忆中的模样 (4)", "与行星相会 (4)", "余生的第一天 (4)", "宇宙市场趋势 (4)", "朗道的选择 (4)", "论剑 (4)", "唯有沉默 (4)", "点个关注吧！ (4)", "「我」的诞生 (4)", "天才们的休憩 (4)", "别让世界静下来 (4)", "鼹鼠党欢迎你 (4)", "在蓝天下 (4)", "秘密誓心 (4)", "决心如汗珠般闪耀 (4)", "猎物的视线 (4)", "晚安与睡颜 (4)", "铭记于心的约定 (4)", "两个人的演唱会 (4)", "无边曼舞 (4)", "谐乐静默之后 (4)", "娜塔莎 (4)", "艾丝妲 (4)", "停云 (4)", "三月七 (4)", "素裳 (4)", "丹恒 (4)", "希露瓦 (4)", "黑塔 (4)", "青雀 (4)", "虎克 (4)", "阿兰 (4)", "佩拉 (4)", "桑博 (4)", "驭空 (4)", "卢卡 (4)", "玲可 (4)", "桂乃芬 (4)", "寒鸦 (4)", "雪衣 (4)", "米沙 (4)", "加拉赫 (4)"}
        fiveStarItems = {"白露 (5)", "布洛妮娅 (5)", "杰帕德 (5)", "彦卿 (5)", "姬子 (5)", "克拉拉 (5)", "瓦尔特 (5)", "时节不居 (5)", "但战斗还未结束 (5)", "制胜的瞬间 (5)", "如泥酣眠 (5)", "银河铁道之夜 (5)", "无可取代的东西 (5)", "以世界之名 (5)"}
        祈愿.Visibility = Visibility.Visible
        姓名列表 = Nothing
    End Sub
End Class
