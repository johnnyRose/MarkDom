using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MarkDom.Tests
{
    [TestClass]
    public class UnitTests
    {
        private static readonly Dictionary<string, string> _testCases = new Dictionary<string, string>()
        {
            { "", "" },
            { " ", "" },
            { "\r\n\r\n \r\n\r\n  ", "" },

            { "#h1", "<h1>h1</h1>" },
            { "# h1", "<h1> h1</h1>" },
            { " #h1", "<p> #h1</p>" },
            { " # h1", "<p> # h1</p>" },
            { "##h2", "<h2>h2</h2>" },
            { "## h2", "<h2> h2</h2>" },
            { " ##h2", "<p> ##h2</p>" },
            { " ## h2", "<p> ## h2</p>" },
            { "###h3", "<h3>h3</h3>" },
            { "### h3", "<h3> h3</h3>" },
            { " ###h3", "<p> ###h3</p>" },
            { " ### h3", "<p> ### h3</p>" },
            { "####h4", "<h4>h4</h4>" },
            { "#### h4", "<h4> h4</h4>" },
            { " ####h4", "<p> ####h4</p>" },
            { " #### h4", "<p> #### h4</p>" },
            { "#####h5", "<h5>h5</h5>" },
            { "##### h5", "<h5> h5</h5>" },
            { " #####h5", "<p> #####h5</p>" },
            { " ##### h5", "<p> ##### h5</p>" },
            { "######h6", "<h6>h6</h6>" },
            { "###### h6", "<h6> h6</h6>" },
            { " ######h6", "<p> ######h6</p>" },
            { " ###### h6", "<p> ###### h6</p>" },

            { "This paragraph talking about C# should not be a header.", "<p>This paragraph talking about C# should not be a header.</p>" },

            { "---", "<hr />" },
            { "***", "<hr />" },
            { "___", "<hr />" },
            { " \n \r \t ---", "<hr />" },
            { " \n \r \t ***", "<hr />" },
            { " \n \r \t ___", "<hr />" },

            { "![alt text](url_goes_here)", "<p><img src=\"url_goes_here\" alt=\"alt text\" /></p>" },
            { "[link text](https://url.com/do_not_italicize)", "<p><a href=\"https://url.com/do_not_italicize\">link text</a></p>" },

            { "*this text is bold!*", "<p><strong>this text is bold!</strong></p>" },
            { "_this text is italicized_", "<p><em>this text is italicized</em></p>" },
            { "_italics_ *bold* _italics_", "<p><em>italics</em> <strong>bold</strong> <em>italics</em></p>" },
            { "_*italics and bold*_", "<p><em><strong>italics and bold</strong></em></p>" },
            { "*_bold and italics_*", "<p><strong><em>bold and italics</em></strong></p>" },

            { "this_is_not_italicized", "<p>this_is_not_italicized</p>" },
            { "for*now*this*can*be*bold*", "<p>for<strong>now</strong>this<strong>can</strong>be<strong>bold</strong></p>" },

            { "`this is inline code`", "<p><code>this is inline code</code></p>" },
            { "this is `partially inline` code", "<p>this is <code>partially inline</code> code</p>" },

            { "~~strikethrough~~", "<p><s>strikethrough</s></p>" },
            { "this is ~~strikethrough and *bold*~~", "<p>this is <s>strikethrough and <strong>bold</strong></s></p>" },

            { "> this is a blockquote", "<blockquote><p> this is a blockquote</p></blockquote>" },
            { ">this is also a blockquote", "<blockquote><p>this is also a blockquote</p></blockquote>" },
            { ">blockquote with ~~strike~~", "<blockquote><p>blockquote with <s>strike</s></p></blockquote>" },

            { "<someHtmlTag>html should just render</someHtmlTag>", "<someHtmlTag>html should just render</someHtmlTag>" },

            { "any remaining text is a paragraph", "<p>any remaining text is a paragraph</p>" },

            {
                "- list item one",

                "<ul><li>list item one</li></ul>"
            },

            {
                @"
                    - item 1
                    - item 2
                        - sub-item 1",
                "<ul><li>item 1</li><li>item 2\r\n<ul><li>sub-item 1</li></ul></li></ul>"
            },

            {
                @"
                    * item 1
                    * item 2
                        * sub-item 1",
                "<ul><li>item 1</li><li>item 2\r\n<ul><li>sub-item 1</li></ul></li></ul>"
            },

            {
                @"
                    1. item 1
                    1. item 2",
                        //10. sub-item 1",
                "<ol><li>item 1</li><li>item 2</li></ol>"
            },

            {
                @"
                    1) item 1
                    1) item 2",
                        //10. sub-item 1",
                "<ol><li>item 1</li><li>item 2</li></ol>"
            },

            { "#heading\r\nparagraph", "<h1>heading</h1><p>paragraph</p>" },

            { "#\r\n\r\n##\n\n###", "<p>#</p><p>##</p><p>###</p>" },

            { "test line 1\r\ntest line 2", "<p>test line 1\r\ntest line 2</p>" },

            { "this is [a link](/)", "<p>this is <a href=\"/\">a link</a></p>" },

            {
                "```\na\n```",
                "<pre><code>a</code></pre>"
            },

            {
                "```\r\na\r\n```",
                "<pre><code>a</code></pre>"
            },

            {
                "```\r\n#not a header\r\n```",
                "<pre><code>#not a header</code></pre>"
            },
        };

        [TestMethod]
        public void Tests()
        {
            foreach (var testCase in _testCases)
            {
                string input = testCase.Key;
                string expectedOutput = testCase.Value;

                string actualOutput = new MarkdownParser().Parse(input).ToHtml();
                Assert.AreEqual(expectedOutput, actualOutput);
            }
        }
    }
}
