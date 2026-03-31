import { Carousel } from "flowbite-react";
import Image from "next/image";

export default function AuctionGallery() {
  return (
    <div className="">
      <Carousel pauseOnHover>
        <div className="relative aspect-video object-cover">
          <Image
            src="https://flowbite.com/docs/images/carousel/carousel-1.svg"
            alt="..."
            fill
            className="object-cover"
            quality={75}
            loading="lazy"
          />
        </div>
        <div className="relative aspect-video object-cover">
          <Image
            src="https://flowbite.com/docs/images/carousel/carousel-1.svg"
            alt="..."
            fill
            className="object-cover"
            quality={75}
            loading="lazy"
          />
        </div>
        <div className="relative aspect-video object-cover">
          <Image
            src="https://flowbite.com/docs/images/carousel/carousel-1.svg"
            alt="..."
            fill
            className="object-cover"
            quality={75}
            loading="lazy"
          />
        </div>
      </Carousel>
    </div>
  );
}
