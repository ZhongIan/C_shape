
# 參考

**簡易計算機**

https://www.youtube.com/watch?v=T5l42z4L3gM

## *註記*

可將多個BUTTON關聯至同一個事件

*(Button)sender* : 轉型 將sender轉成Button

sender代表的是引發這個事件的Object，所以我們可以把他轉型回來使用

```C#
private void button1_Click(object sender, EventArgs e)
{
    MessageBox.Show ( ( (Button)sender).Name.ToString());
}
```

如果當你有很多不同的button都把click事件委派到同一個方法，但是中間可能又要辨認是哪一個button引發時就很有用了


[Sender是在幹嘛的 ?](https://ithelp.ithome.com.tw/articles/10032726)

