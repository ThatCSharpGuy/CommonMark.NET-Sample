using CommonMark;
using CommonMark.Syntax;

namespace CommonMarkNETSample
{
	class CustomHtmlFormatter : CommonMark.Formatters.HtmlFormatter
	{
	    public CustomHtmlFormatter(System.IO.TextWriter target, CommonMarkSettings settings)
	        : base(target, settings)
	    {
	    }

		protected override void WriteBlock(Block block, bool isOpening, bool isClosing, out bool ignoreChildNodes)
		{
			// Si es un tag de apertura y es Document
			if (isOpening && block.Tag == BlockTag.Document)
			{
				this.Write("<html>");
				this.Write("<head>\n<meta charset=\"UTF-8\">\n<link href=\"https://github.com/jasonm23/markdown-css-themes/raw/gh-pages/markdown6.css\" rel=\"stylesheet\" ></head>");
				this.Write("<body>");
			}
			// Si es un tag de cierre y es Document
			else if (isClosing && block.Tag == BlockTag.Document)
			{
				this.Write("</body></html>");
			}

			// LLamamos a la implementación por default para procesar los otros nodos
			base.WriteBlock(block, isOpening, isClosing, out ignoreChildNodes);
		}

	    protected override void WriteInline(Inline inline, bool isOpening, bool isClosing, out bool ignoreChildNodes)
	    {
			if (inline.Tag == InlineTag.Link) // Es enlace
	        {
	            ignoreChildNodes = false; // Queremos seguir procesando los nodos hijo

				//  Revisamos si es la etiqueta de apertura
				if (isOpening)
	            {
	                this.Write("<a target=\"_blank\" href=\"");
	                this.WriteEncodedUrl(inline.TargetUrl);
	                this.Write("\">");
	            }

				//  Revisamos si es la etiqueta de cierre
				if (isClosing)
	            {
	                this.Write("</a>");
	            }
	        }
	        else
	        {
	            // Usamos la implementación por default para otro los otros nodos
	            base.WriteInline(inline, isOpening, isClosing, out ignoreChildNodes);
	        }
	    }
	}
}