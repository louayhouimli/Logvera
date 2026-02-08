import Link from "next/link";

import {
  Accordion,
  AccordionContent,
  AccordionItem,
  AccordionTrigger,
} from "@/components/ui/accordion";
import { cn } from "@/lib/utils";

const categories = [
  {
    title: "Getting Started",
    questions: [
      {
        question: "What is Logvera and what problem does it solve?",
        answer:
          "Logvera is an open-source API observability platform that helps you ingest, analyze, and monitor API logs, define alert rules, and gain visibility into system behavior through analytics and alerts.",
      },
      {
        question: "Do I need to modify my existing APIs to use Logvera?",
        answer:
          "No. Logvera is API-first and non-intrusive. You can integrate it using an API key or the provided SDK without changing your existing API logic.",
      },
      {
        question: "Is Logvera suitable for small projects or only large systems?",
        answer:
          "Logvera is designed to work for both small services and larger systems. You can start with a single API and scale to multiple services as needed.",
      },
    ],
  },
  {
    title: "Logs & Ingestion",
    questions: [
      {
        question: "How are logs ingested into Logvera?",
        answer:
          "Logs are ingested through a secure API endpoint using API key authentication. Each API key is scoped to a specific API to ensure isolation and ownership.",
      },
      {
        question: "What kind of data can I send as logs?",
        answer:
          "You can send structured API logs including endpoint, HTTP method, status code, duration, and timestamp. Logvera normalizes this data for consistent querying and analytics.",
      },
      {
        question: "Is there an SDK available?",
        answer:
          "Yes. Logvera provides a lightweight SDK that standardizes log submission and abstracts away ingestion details, improving developer experience.",
      },
    ],
  },
  {
    title: "Alerts & Monitoring",
    questions: [
      {
        question: "How does the alerting system work?",
        answer:
          "Alert rules are defined using thresholds and time windows. These rules are continuously evaluated by a background service, and alerts are triggered when conditions are met.",
      },
      {
        question: "How does Logvera prevent alert spam?",
        answer:
          "Logvera applies cooldown logic to ensure that alerts are not repeatedly triggered while the same condition persists.",
      },
      {
        question: "Can I view past alerts?",
        answer:
          "Yes. All triggered alerts are stored and can be viewed later, including their read/unread state.",
      },
    ],
  },
  {
    title: "Security & Access",
    questions: [
      {
        question: "How is access to data secured?",
        answer:
          "User access is secured using JWT authentication, while log ingestion uses API keys. All resources are strictly scoped to their owners.",
      },
      {
        question: "Can multiple APIs be monitored under one account?",
        answer:
          "Yes. You can register and monitor multiple APIs under a single account, with clear separation between each API’s data.",
      },
    ],
  },
];


export const FAQ = ({
  headerTag = "h2",
  className,
  className2,
}: {
  headerTag?: "h1" | "h2";
  className?: string;
  className2?: string;
}) => {
  return (
    <section className={cn("py-28 lg:py-32", className)}>
      <div className="container max-w-5xl">
        <div className={cn("mx-auto grid gap-16 lg:grid-cols-2", className2)}>
          <div className="space-y-4">
            {headerTag === "h1" ? (
              <h1 className="text-2xl tracking-tight md:text-4xl lg:text-5xl">
                Got Questions?
              </h1>
            ) : (
              <h2 className="text-2xl tracking-tight md:text-4xl lg:text-5xl">
                Got Questions?
              </h2>
            )}
            <p className="text-muted-foreground max-w-md leading-snug lg:mx-auto">
              If you can't find what you're looking for,{" "}
              <Link href="/contact" className="underline underline-offset-4">
                get in touch
              </Link>
              .
            </p>
          </div>

          <div className="grid gap-6 text-start">
            {categories.map((category, categoryIndex) => (
              <div key={category.title} className="">
                <h3 className="text-muted-foreground border-b py-4">
                  {category.title}
                </h3>
                <Accordion type="single" collapsible className="w-full">
                  {category.questions.map((item, i) => (
                    <AccordionItem key={i} value={`${categoryIndex}-${i}`}>
                      <AccordionTrigger>{item.question}</AccordionTrigger>
                      <AccordionContent className="text-muted-foreground">
                        {item.answer}
                      </AccordionContent>
                    </AccordionItem>
                  ))}
                </Accordion>
              </div>
            ))}
          </div>
        </div>
      </div>
    </section>
  );
};
