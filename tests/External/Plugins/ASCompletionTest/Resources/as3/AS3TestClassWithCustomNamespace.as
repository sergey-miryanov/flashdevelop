package flashdevelop.tests {
	
	/**
	 * @author SlavaRa
	 */
	public class TestClass {

		use namespace $private;

		$private function test():void {
			trace("$private.test");
		}
	}
}

namespace $private = "flashdevelop.tests.$private";