using System;
using MbDotNet.Models.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MbDotNet.Tests.Client

{
	[TestClass, TestCategory("Unit")]

	public class GetEntryHypermediaTest : MountebankClientTestBase
	{
		[TestMethod]

		public async Task ReturnsEntryHypermedia()
		{

			var expectedResult = new Home()
			{

				Links = new Link()
				{
					Imposters = new HrefField(),
					Config = new HrefField(),
					Logs = new HrefField(),

				}

			};


			MockRequestProxy.Setup(x => x.GetEntryHypermediaAsync(default)).ReturnsAsync(expectedResult);
			var result = await Client.GetEntryHypermediaAsync().ConfigureAwait(false);

			Assert.AreSame(expectedResult, result);
		}
	}
}



