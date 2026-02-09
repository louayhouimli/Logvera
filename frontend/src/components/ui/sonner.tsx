"use client";

import {
  CircleCheckIcon,
  InfoIcon,
  Loader2Icon,
  OctagonXIcon,
  TriangleAlertIcon,
} from "lucide-react";
import { useTheme } from "next-themes";
import Image from "next/image";
import { Toaster as Sonner, type ToasterProps } from "sonner";

const LogveraIcon = (
  <Image
    className="dark:invert"
    src="/logo.svg"
    alt="Logo"
    width={24}
    height={24}
  />
);

const Toaster = ({ ...props }: ToasterProps) => {
  const { theme = "system" } = useTheme();

  return (
    <Sonner
      position="top-center"
      theme={theme as ToasterProps["theme"]}
      className="toaster group"
      icons={{
        success: LogveraIcon,
        info: LogveraIcon,
        warning: LogveraIcon,
        error: LogveraIcon,
        loading: LogveraIcon,
      }}
      style={
        {
          "--normal-bg": "var(--popover)",
          "--normal-text": "var(--popover-foreground)",
          "--normal-border": "var(--border)",
          "--border-radius": "var(--radius)",
        } as React.CSSProperties
      }
      {...props}
    />
  );
};

export { Toaster };
