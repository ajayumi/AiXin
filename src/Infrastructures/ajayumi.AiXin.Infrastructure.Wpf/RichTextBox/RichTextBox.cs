﻿#region Copyright
//************************************************************************************/
//	创建人员(Creator)    ：ajayumi
//	创建日期(Date)       ：2016-11-20
//  联系方式(Email)      ：aj-ayumi@163.com; gajayumi@gmail.com;
//  描    述(Description)：
//
//	Copyright(C) 2009-2016 ajayumi.All rights reserved.
//************************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;

namespace ajayumi.AiXin.Infrastructure.Wpf
{
    public class RichTextBox : System.Windows.Controls.RichTextBox
    {
        private DispatcherTimer _timer;

        public RichTextBox()
        {
            Emoticons = new EmoticonCollection();
        }

        /// <summary>
        /// Override to trigger the look up.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            //Looking for an idle time to start look up..
            if (_timer == null)
            {
                _timer = new DispatcherTimer(DispatcherPriority.Background);
                _timer.Interval = TimeSpan.FromSeconds(0.5);
                _timer.Tick += LookUp;
            }

            //Restart timer here...
            _timer.Stop();
            _timer.Start();

            base.OnTextChanged(e);
        }

        private void LookUp(object sender, EventArgs e)
        {
            Dispatcher.InvokeAsync(UpdateSmileys);
            _timer.Stop();
        }

        /// <summary>
        /// Iterate through words and get the text range of smiley text. Replace it with corresponding icon.
        /// </summary>
        private void UpdateSmileys()
        {
            var tp = Document.ContentStart;
            var word = WordBreaker.GetWordRange(tp);

            while (word.End.GetNextInsertionPosition(LogicalDirection.Forward) != null)
            {
                word = WordBreaker.GetWordRange(word.End.GetNextInsertionPosition(LogicalDirection.Forward));
                var smileys = from smiley in Emoticons
                              where smiley.Text == word.Text
                              select smiley;

                var emoticonMappers = smileys as IList<EmoticonMapper> ?? smileys.ToList();
                if (emoticonMappers.Any())
                {
                    var emoticon = emoticonMappers.FirstOrDefault();
                    var img = new Image() { Stretch = Stretch.None };
                    if (emoticon != null) img.Source = emoticon.Icon;
                    ReplaceTextRangeWithImage(word, img);
                }
            }
        }

        /// <summary>
        /// Replacing the text range with image.
        /// </summary>
        /// <param name="textRange">The smiley text range.</param>
        /// <param name="image">The smiley icon</param>
        public void ReplaceTextRangeWithImage(TextRange textRange, Image image)
        {
            if (textRange.Start.Parent is Run)
            {
                var run = textRange.Start.Parent as Run;

                var runBefore = new Run(new TextRange(run.ContentStart, textRange.Start).Text);
                var runAfter = new Run(new TextRange(textRange.End, run.ContentEnd).Text);

                if (textRange.Start.Paragraph != null)
                {
                    textRange.Start.Paragraph.Inlines.Add(runBefore);
                    textRange.Start.Paragraph.Inlines.Add(image);
                    textRange.Start.Paragraph.Inlines.Add(runAfter);
                    textRange.Start.Paragraph.Inlines.Remove(run);
                }

                CaretPosition = runAfter.ContentEnd;

            }
        }

        /// <summary>
        /// The collection of Emoticon mappers
        /// </summary>
        public EmoticonCollection Emoticons { get; set; }
    }

}

