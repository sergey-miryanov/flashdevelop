package flashdevelop.tests {
	
	/**
	 * @author SlavaRa
	 */
	public class TestClass {

		use namespace $private_1;
		use namespace $private_2;

		$private_1 function test():void {
			trace("$private_1.test");
		}

		$private_2 function test():void {
			trace("$private_2.test");
		}
	}
}

namespace $private_1 = "flashdevelop.tests.$private_1";
namespace $private_2 = "flashdevelop.tests.$private_2";