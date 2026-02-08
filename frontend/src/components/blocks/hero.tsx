import Image from "next/image";

import {
  ArrowRight,
  ArrowUpRight,
  Blend,
  ChartNoAxesColumn,
  CircleDot,
  Diamond,
} from "lucide-react";

import { DashedLine } from "@/components/dashed-line";
import { Button } from "@/components/ui/button";
import Link from "next/link";

const features = [
  {
    title: "Logs & Analytics",
    description: "Ingest structured API logs, explore them efficiently, and analyze request volume, errors, and latency over time.",
    icon: CircleDot,
  },
  {
    title: "Multi-API Monitoring",
    description: "Monitor multiple APIs and services under a single account, with strict ownership and isolation per project.",
    icon: Blend,
  },
  {
    title: "Alert Rules",
    description: "Define alert rules based on error rates and time windows, continuously evaluated in the background.",
    icon: Diamond,
  },
  {
    title: "Alerts & Visibility",
    description: "Receive clear, actionable alerts when system behavior deviates from expected thresholds.",
    icon: ChartNoAxesColumn,
  },
];

export const Hero = () => {
  return (
    <section className="py-28 lg:py-32 lg:pt-44">
      <div className="container flex flex-col justify-between gap-8 md:gap-14 lg:flex-row lg:gap-20">
        {/* Left side - Main content */}
        <div className="flex-1">
          <h1 className="text-foreground max-w-160 text-3xl tracking-tight md:text-4xl lg:text-5xl xl:whitespace-nowrap">
            Logvera
          </h1>

          <p className="text-muted-foreground text-1xl mt-5 md:text-3xl">
            An open-source platform for ingesting, analyzing, and monitoring API logs at scale.
          </p>

          <div className="mt-8 flex flex-wrap items-center gap-4 lg:flex-nowrap">
            <Button asChild>
              <Link href="/login">
               Get Started <ArrowUpRight className="size-4" />
              </Link>
            </Button>
           
          </div>
        </div>

        {/* Right side - Features */}
        <div className="relative flex flex-1 flex-col justify-center space-y-5 max-lg:pt-10 lg:pl-10">
          <DashedLine
            orientation="vertical"
            className="absolute top-0 left-0 max-lg:hidden"
          />
          <DashedLine
            orientation="horizontal"
            className="absolute top-0 lg:hidden"
          />
          {features.map((feature) => {
            const Icon = feature.icon;
            return (
              <div key={feature.title} className="flex gap-2.5 lg:gap-5">
                <Icon className="text-foreground mt-1 size-4 shrink-0 lg:size-5" />
                <div>
                  <h2 className="font-text text-foreground font-semibold">
                    {feature.title}
                  </h2>
                  <p className="text-muted-foreground max-w-76 text-sm">
                    {feature.description}
                  </p>
                </div>
              </div>
            );
          })}
        </div>
      </div>

      <div className="mt-12 max-lg:ml-6 max-lg:h-[550px] max-lg:overflow-hidden md:mt-20 lg:container lg:mt-24">
        <div className="relative h-[793px] w-full">
          <Image
            src="/hero.webp"
            alt="hero"
            fill
            className="rounded-2xl object-cover object-left-top shadow-lg max-lg:rounded-tr-none"
          />
        </div>
      </div>
    </section>
  );
};
