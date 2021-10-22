using System;
using System.Text;

namespace project1
{

	public class pro
	{
		public static void Main()
		{
			StringBuilder sb = new StringBuilder("vishwa ");

			sb.Append("World!!" + "Hey" );
			sb.AppendLine("Nice day");

			Console.WriteLine(sb);
		}
	}
}
